import { Component, Input, OnInit, OnChanges, SimpleChanges, ChangeDetectorRef, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzUploadModule, NzUploadFile, NzUploadChangeParam } from 'ng-zorro-antd/upload';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzDescriptionsModule } from 'ng-zorro-antd/descriptions';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzMessageService, NzMessageModule } from 'ng-zorro-antd/message';
import { AdminService } from '../../services/admin.service';

export enum MailTypes {
    NetMail = 1,
    OutLookMail = 2,
    LotusNotesMail = 3,
    Internal = 4
}

export enum AdditionalDataState {
    Unchanged = 'Unchanged',
    Added = 'Added',
    Deleted = 'Deleted',
    Modified = 'Modifyed'
}

// Trigger rebuild
@Component({
    selector: 'user-data',
    templateUrl: './user-data.component.html',
    styleUrls: ['./user-data.css'],
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NzFormModule,
        NzInputModule,
        NzCheckboxModule,
        NzButtonModule,
        NzGridModule,
        NzTableModule,
        NzIconModule,
        NzCardModule,
        NzSpaceModule,
        NzSelectModule,
        NzUploadModule,
        NzModalModule,
        NzDescriptionsModule,
        NzTypographyModule,
        NzTagModule,
        NzDividerModule,
        NzInputNumberModule,
        NzMessageModule
    ]
})
export class UserDataComponent implements OnInit, OnChanges {
    @Input() user!: any;
    @Output() onUserUpdated = new EventEmitter<any>();
    userForm!: FormGroup;
    passwordForm!: FormGroup;
    newDataTypeForm!: FormGroup;

    isLoading = false;
    isEditing = false;
    isChangePasswordVisible = false;
    isNewDataTypeVisible = false;
    isCreatingDataType = false;
    passwordVisible = false;
    mailPasswordVisible = false;

    MailTypes = MailTypes;
    mailTypeOptions = [
        { label: 'NetMail', value: MailTypes.NetMail },
        { label: 'OutLookMail', value: MailTypes.OutLookMail },
        { label: 'LotusNotesMail', value: MailTypes.LotusNotesMail },
        { label: 'Internal', value: MailTypes.Internal }
    ];

    AdditionalDataState = AdditionalDataState;

    toggleEdit() {
        if (this.isEditing) {
            this.loadUserData(this.user);
        }
        this.isEditing = !this.isEditing;
    }

    showChangePasswordModal(): void {
        this.passwordForm.reset();
        this.isChangePasswordVisible = true;
    }

    handleCancelPasswordModal(): void {
        this.isChangePasswordVisible = false;
        this.passwordForm.reset();
    }

    handleChangePassword(): void {
        if (this.passwordForm.valid) {
            const formValue = this.passwordForm.getRawValue();

            const userId = this.user._id;
            const username = this.user.name || this.user._name;
            const newPassword = formValue.newPassword;

            this.isLoading = true;
            this.adminService.changePassword(userId, username, newPassword).subscribe({
                next: (result) => {
                    this.isLoading = false;
                    if (result && result.IsValid) {
                        this.message.success('Password changed successfully');
                        this.isChangePasswordVisible = false;
                        this.passwordForm.reset();
                    } else {
                        this.message.error(result.Message || 'Error changing password');
                    }
                    this.cdr.detectChanges();
                },
                error: (err) => {
                    this.isLoading = false;
                    this.message.error('Server error');
                    console.error('Change password error:', err);
                    this.cdr.detectChanges();
                }
            });
        } else {
            Object.values(this.passwordForm.controls).forEach(control => {
                if (control.invalid) {
                    control.markAsDirty();
                    control.updateValueAndValidity({ onlySelf: true });
                }
            });
        }
    }


    // Mock data for additional user data table
    additionalData: any[] = [];
    dataTypes: any[] = [];

    // Upload lists
    fileListPhoto: NzUploadFile[] = [];
    fileListSignature: NzUploadFile[] = [];

    constructor(private fb: FormBuilder,
        private cdr: ChangeDetectorRef,
        private adminService: AdminService,
        private message: NzMessageService) { }

    ngOnInit(): void {
        this.initForm();
        this.initPasswordForm();
        this.initNewDataTypeForm();
        this.loadDataTypes();
        if (this.user) {
            this.loadUserData(this.user);
        }
    }

    loadDataTypes() {
        this.adminService.getDataTypes().subscribe({
            next: (result) => {
                this.dataTypes = result;
            },
            error: (err) => {
                console.error('Error loading data types:', err);
            }
        });
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['user'] && changes['user'].currentValue) {
            this.loadUserData(changes['user'].currentValue);
        }
    }

    private initForm(): void {
        this.userForm = this.fb.group({
            username: [''],
            id: [{ value: '', disabled: true }],
            blocked: [false],
            firstName: [''],
            lastName: [''],
            password: [''],
            confirmPassword: [''],
            phone: [''],
            position: [''],
            mailAccount: [''],
            mailUser: [''],
            mailPassword: [''],
            mailSsl: [false],
            mailPort: [25],
            mailSmtp: [''],
            mailServer: [''],
            mailBase: [''],
            mailType: [MailTypes.NetMail]
            // photoPath: [''],
            // signaturePath: ['']
        });
    }

    private initPasswordForm(): void {
        this.passwordForm = this.fb.group({
            // Validate that password only contains letters, numbers, and common symbols
            newPassword: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9!@#\$%\^&\*\(\)_\+\-=\[\]\{\};':"\\|,.<>\/?]*$/)]],
            confirmPassword: ['', [Validators.required]],
            sendByEmail: [false]
        });
    }
    private initNewDataTypeForm(): void {
        this.newDataTypeForm = this.fb.group({
            name: ['', [Validators.required, Validators.maxLength(100), Validators.pattern(/^[a-zA-Z0-9\sñÑáéíóúÁÉÍÓÚ\-_]*$/)]]
        });
    }
    loadUserData(user: any) {
        if (this.userForm && user) {
            this.userForm.reset({
                id: user._id,
                username: user.name || user._name,
                firstName: user._nombres,
                lastName: user._apellidos,
                password: user._password,
                // confirmPassword: user._password, 
                phone: user._telefono,
                position: user._puesto,
                mailAccount: user._email?._mail,
                mailSsl: user._email?._enableSsl,
                mailPassword: user._email?._password,
                mailPort: user._email?._puerto,
                mailUser: user._email?._userName,
                mailSmtp: user._email?._proveedorSMTP,
                mailType: user._email?._type,
                mailServer: user._email?._servidor,
                mailBase: user._email?._base,
                blocked: user.blocked ?? false
            });

            if (user._id) {
                this.adminService.getAdditionalData(user._id).subscribe({
                    next: (result: any[]) => {
                        console.log('Additional data loaded:', result);
                        this.additionalData = result.map(item => ({
                            title: item.Title,
                            value: item.Value,
                            id: item.Id,
                            dataTypeId: item.DataTypeId,
                            state: AdditionalDataState.Unchanged
                        }));
                        this.cdr.detectChanges();
                    },
                    error: (err) => {
                        console.error('Error loading additional data:', err);
                    }
                });
            } else {
                this.additionalData = [];
            }
        }
        // Mock default photo
        /*
        this.fileListPhoto = [
            {
                uid: '-1',
                name: 'profile_photo.png',
                status: 'done',
                url: 'https://zos.alipayobjects.com/rmsportal/jkjgkEfvpUPVyRjUImniVslZfWPnJuuZ.png'
            }
        ];
        */
    }

    handlePhotoChange(info: NzUploadChangeParam): void {
        // Handle upload logic
        console.log('Photo changed:', info);
    }

    handleSignatureChange(info: NzUploadChangeParam): void {
        // Handle upload logic
        console.log('Signature changed:', info);
    }

    onSubmit() {
        if (this.userForm.invalid) {
            this.markFormAsDirty();
            return;
        }

        this.isLoading = true;
        const formValue = this.userForm.getRawValue();

        const isMailConfigChanged = this.checkMailConfigChanged();
        const isGeneralDataChanged = this.checkGeneralDataChanged();
        const additionalDataResult = this.processAdditionalData();
        console.log(additionalDataResult);

        const payload = this.buildPayload(formValue, isMailConfigChanged, isGeneralDataChanged, additionalDataResult);

        this.adminService.updateUserData(payload).subscribe({
            next: (res: any) => this.handleUpdateSuccess(formValue, isMailConfigChanged, isGeneralDataChanged, additionalDataResult.data),
            error: (err: any) => this.handleUpdateError(err)
        });
    }

    private markFormAsDirty() {
        Object.values(this.userForm.controls).forEach(control => {
            if (control.invalid) {
                control.markAsDirty();
                control.updateValueAndValidity({ onlySelf: true });
            }
        });
    }

    private checkMailConfigChanged(): boolean {
        const mailFields = ['mailAccount', 'mailUser', 'mailPassword', 'mailSsl', 'mailPort', 'mailSmtp', 'mailServer', 'mailBase', 'mailType'];
        return mailFields.some(field => this.userForm.get(field)?.dirty);
    }

    private checkGeneralDataChanged(): boolean {
        const generalFields = ['username', 'blocked', 'firstName', 'lastName', 'phone', 'position'];
        return generalFields.some(field => this.userForm.get(field)?.dirty);
    }

    private processAdditionalData(): { changed: boolean, data: any[] } {
        let isChanged = false;
        const processedData: any[] = [];

        this.additionalData.forEach(item => {
            if (item.state === AdditionalDataState.Deleted) {
                isChanged = true;
                processedData.push(item);
                return;
            }

            const trimmedValue = item.value ? item.value.trim() : '';
            const hasTitle = !!item.title;
            const isValid = hasTitle && trimmedValue !== '';

            if (isValid) {
                if (item.state !== AdditionalDataState.Unchanged) {
                    isChanged = true;
                }

                // Ensure DataTypeId is populated if missing (double check)
                let currentDataTypeId = item.dataTypeId;
                if (!currentDataTypeId && this.dataTypes) {
                    const selectedType = this.dataTypes.find(t => (t.Title || t.Name) === item.title);
                    if (selectedType) {
                        currentDataTypeId = selectedType.Id || selectedType.id || selectedType.ID || selectedType.DataTypeId || 0;
                    }
                }

                processedData.push({
                    ...item,
                    value: trimmedValue,
                    dataTypeId: currentDataTypeId || 0
                });
            } else {
                // If it's an existing item that became invalid (empty), treat as deleted
                if (item.state !== AdditionalDataState.Added) {
                    isChanged = true;
                    processedData.push({
                        ...item,
                        state: AdditionalDataState.Deleted
                    });
                }
            }
        });

        return { changed: isChanged, data: processedData };
    }

    private buildPayload(formValue: any, isMailConfigChanged: boolean, isGeneralDataChanged: boolean, additionalDataResult: { changed: boolean, data: any[] }) {
        // Map to PascalCase for the API
        const additionalDataPayload = additionalDataResult.data.map(item => ({
            Id: item.id || 0,
            Title: item.title,
            Value: item.value,
            DataTypeId: item.dataTypeId || 0,
            State: item.state
        }));

        return {
            "Usuario": formValue.username,
            "ID": formValue.id,
            "Bloqueado": formValue.blocked,
            "Nombres": formValue.firstName,
            "Apellidos": formValue.lastName,
            "Telefonos": formValue.phone,
            "Puesto": formValue.position,
            "MailAccount": formValue.mailAccount,
            "MailUser": formValue.mailUser,
            "MailPassword": formValue.mailPassword,
            "MailSsl": formValue.mailSsl,
            "MailPort": formValue.mailPort,
            "MailSmtp": formValue.mailSmtp,
            "MailServer": formValue.mailServer,
            "MailBase": formValue.mailBase,
            "MailType": formValue.mailType,
            "UpdateMailConfig": isMailConfigChanged,
            "UpdateGeneralData": isGeneralDataChanged,
            "UpdateAdditionalData": additionalDataResult.changed,
            "AdditionalData": additionalDataResult.changed ? JSON.stringify(additionalDataPayload) : null
        };
    }

    private handleUpdateSuccess(formValue: any, isMailConfigChanged: boolean, isGeneralDataChanged: boolean, processedAdditionalData: any[]) {
        this.isEditing = false;
        this.isLoading = false;

        this.updateLocalAdditionalData(processedAdditionalData);
        if (this.user) {
            if (isMailConfigChanged) this.updateLocalMailConfig(formValue);
            if (isGeneralDataChanged) this.updateLocalGeneralData(formValue);
        }

        this.onUserUpdated.emit(this.user);
        this.cdr.detectChanges();
    }

    private handleUpdateError(err: any) {
        console.error('Update failed', err);
        this.isLoading = false;
    }

    private updateLocalAdditionalData(processedData: any[]) {
        this.additionalData = processedData
            .filter(item => item.state !== AdditionalDataState.Deleted)
            .map(item => ({ ...item, state: AdditionalDataState.Unchanged }));
    }

    private updateLocalMailConfig(formValue: any) {
        if (!this.user._email) {
            this.user._email = {};
        }
        this.user._email._mail = formValue.mailAccount;
        this.user._email._userName = formValue.mailUser;
        this.user._email._password = formValue.mailPassword;
        this.user._email._enableSsl = formValue.mailSsl;
        this.user._email._puerto = formValue.mailPort;
        this.user._email._proveedorSMTP = formValue.mailSmtp;
        this.user._email._servidor = formValue.mailServer;
        this.user._email._base = formValue.mailBase;
        this.user._email._type = formValue.mailType;
    }

    private updateLocalGeneralData(formValue: any) {
        this.user._name = formValue.username;
        this.user.name = formValue.username;
        this.user._nombres = formValue.firstName;
        this.user._apellidos = formValue.lastName;
        this.user._telefono = formValue.phone;
        this.user.blocked = formValue.blocked;
    }

    resetPassword() {
        console.log('Reset Password Clicked');
    }

    configure() {
        console.log('Configure Clicked');
    }

    createNewDataType() {
        this.additionalData = [...this.additionalData, { title: '', value: '', state: AdditionalDataState.Added }];
    }

    showNewDataTypeModal(): void {
        this.newDataTypeForm.reset();
        this.isNewDataTypeVisible = true;
    }

    handleCancelNewDataType(): void {
        this.isNewDataTypeVisible = false;
        this.newDataTypeForm.reset();
    }

    handleOkNewDataType(): void {
        if (this.newDataTypeForm.valid) {
            this.isCreatingDataType = true;
            const name = this.newDataTypeForm.get('name')?.value;
            this.adminService.saveAdditionalDataType(name).subscribe({
                next: (res) => {
                    this.isCreatingDataType = false;
                    if (res === true) {
                        this.isNewDataTypeVisible = false;
                        this.message.success('Tipo de dato creado correctamente');
                        this.loadDataTypes();
                    } else {
                        this.message.error('Ha ocurrido un error al crear el tipo de dato');
                    }
                },
                error: (err) => {
                    this.isCreatingDataType = false;
                    this.message.error('Error al crear el tipo de dato');
                    console.error(err);
                }
            });
        } else {
            Object.values(this.newDataTypeForm.controls).forEach(control => {
                if (control.invalid) {
                    control.markAsDirty();
                    control.updateValueAndValidity({ onlySelf: true });
                }
            });
        }
    }

    deleteAdditionalData(item: any) {
        if (item.state === AdditionalDataState.Added) {
            const index = this.additionalData.indexOf(item);
            if (index > -1) {
                this.additionalData = this.additionalData.filter((_, i) => i !== index);
            }
        } else {
            item.state = AdditionalDataState.Deleted;
        }
    }

    onAdditionalDataChange(item: any) {
        if (item.state === AdditionalDataState.Unchanged) {
            item.state = AdditionalDataState.Modified;
        }

        if (this.dataTypes) {
            const selectedType = this.dataTypes.find(t => (t.Title || t.Name) === item.title);
            console.log('Selected Type Lookup:', { title: item.title, found: selectedType, allTypes: this.dataTypes });

            if (selectedType) {
                item.dataTypeId = selectedType.Id || selectedType.id || selectedType.ID || selectedType.DataTypeId || 0;
                console.log('Assigned DataTypeId:', item.dataTypeId);
            }
        }
    }

    getMailTypeLabel(value: number): string {
        const option = this.mailTypeOptions.find(opt => opt.value === value);
        return option ? option.label : '';
    }
}
