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
import { AdminService } from '../../services/admin.service';

export enum MailTypes {
    NetMail = 1,
    OutLookMail = 2,
    LotusNotesMail = 3,
    Internal = 4
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
        NzInputNumberModule
    ]
})
export class UserDataComponent implements OnInit, OnChanges {
    @Input() user!: any;
    @Output() onUserUpdated = new EventEmitter<any>();
    userForm!: FormGroup;
    passwordForm!: FormGroup;

    isLoading = false;
    isEditing = false;
    isChangePasswordVisible = false;
    passwordVisible = false;
    mailPasswordVisible = false;

    MailTypes = MailTypes;
    mailTypeOptions = [
        { label: 'NetMail', value: MailTypes.NetMail },
        { label: 'OutLookMail', value: MailTypes.OutLookMail },
        { label: 'LotusNotesMail', value: MailTypes.LotusNotesMail },
        { label: 'Internal', value: MailTypes.Internal }
    ];

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
    }

    handleChangePassword(): void {
        if (this.passwordForm.valid) {
            // Implement password change logic here
            console.log('Password changed:', this.passwordForm.value);
            this.isChangePasswordVisible = false;
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
    additionalData: Array<{ title: string; value: string }> = [];

    // Upload lists
    fileListPhoto: NzUploadFile[] = [];
    fileListSignature: NzUploadFile[] = [];

    constructor(private fb: FormBuilder,
        private cdr: ChangeDetectorRef,
        private adminService: AdminService) { }

    ngOnInit(): void {
        this.initForm();
        this.initPasswordForm();
        if (this.user) {
            this.loadUserData(this.user);
        }
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
            newPassword: ['', [Validators.required]],
            confirmPassword: ['', [Validators.required]]
        });
    }

    loadUserData(user: any) {
        console.log('Loading user data:', user);
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
        if (this.userForm.valid) {
            this.isLoading = true;
            console.log('Form Submitted', this.userForm.value);
            const formValue = this.userForm.getRawValue();

            // Detect changes in mail configuration
            const mailFields = ['mailAccount', 'mailUser', 'mailPassword', 'mailSsl', 'mailPort', 'mailSmtp', 'mailServer', 'mailBase', 'mailType'];
            const isMailConfigChanged = mailFields.some(field => this.userForm.get(field)?.dirty);

            // Detect changes in general data
            const generalFields = ['username', 'blocked', 'firstName', 'lastName', 'phone', 'position'];
            const isGeneralDataChanged = generalFields.some(field => this.userForm.get(field)?.dirty);

            const payload = {
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
                "UpdateGeneralData": isGeneralDataChanged
            };

            this.adminService.updateUserData(payload).subscribe({
                next: (res: any) => {
                    console.log('Update successful', res);
                    this.isEditing = false;
                    this.isLoading = false;

                    if (isMailConfigChanged && this.user) {
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

                    if (isGeneralDataChanged && this.user) {
                        this.user._name = formValue.username;
                        this.user.name = formValue.username;
                        this.user._nombres = formValue.firstName;
                        this.user._apellidos = formValue.lastName;
                        this.user._telefono = formValue.phone;
                        this.user.blocked = formValue.blocked;
                    }

                    this.onUserUpdated.emit(this.user);
                    this.cdr.detectChanges();
                },
                error: (err: any) => {
                    console.error('Update failed', err);
                    this.isLoading = false;
                }
            });
        } else {
            Object.values(this.userForm.controls).forEach(control => {
                if (control.invalid) {
                    control.markAsDirty();
                    control.updateValueAndValidity({ onlySelf: true });
                }
            });
        }
    }

    resetPassword() {
        console.log('Reset Password Clicked');
    }

    configure() {
        console.log('Configure Clicked');
    }

    createNewDataType() {
        console.log('Create New Data Type Clicked');
        // Example: add a row
        this.additionalData = [...this.additionalData, { title: 'Nuevo Dato', value: '' }];
    }

    getMailTypeLabel(value: number): string {
        const option = this.mailTypeOptions.find(opt => opt.value === value);
        return option ? option.label : '';
    }
}
