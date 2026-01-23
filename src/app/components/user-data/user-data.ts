import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
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
    userForm!: FormGroup;
    passwordForm!: FormGroup;

    isEditing = false;
    isChangePasswordVisible = false;
    passwordVisible = false;
    confirmPasswordVisible = false;

    toggleEdit() {
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

    constructor(private fb: FormBuilder) { }

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
            mailType: ['NetMail']
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
            this.userForm.patchValue({
                id: user._id,
                username: user.name || user._name,
                firstName: user._nombres,
                lastName: user._apellidos,
                password: user._password,
                // confirmPassword: user._password, // Usually we don't prefill confirm password or we set it same
                phone: user._telefono,
                // position: // Not provided in mapping
            });
            // Handle blocked if it exists in user object, otherwise default
            // this.userForm.patchValue({ blocked: user.blocked });
        }
        // Mock default photo
        this.fileListPhoto = [
            {
                uid: '-1',
                name: 'profile_photo.png',
                status: 'done',
                url: 'https://zos.alipayobjects.com/rmsportal/jkjgkEfvpUPVyRjUImniVslZfWPnJuuZ.png'
            }
        ];
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
            console.log('Form Submitted', this.userForm.value);
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
}
