import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RecurrenceType, ScheduleConfigModel, ScheduleEventModel } from '../schedule.model';
import { RecurrenceTypeOption, ScheduleService } from '../schedule.service';

import { NzMessageService } from 'ng-zorro-antd/message';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-schedule-form',
  templateUrl: './schedule-form.component.html',
  styleUrls: ['./schedule-form.component.css']
})
export class ScheduleFormComponent implements OnInit, OnDestroy {
  form!: FormGroup;
  loading = false;
  isEditMode = false;
  scheduleId: number | null = null;
  recurrenceTypes: RecurrenceTypeOption[] = [];
  currentRecurrenceType: RecurrenceType = RecurrenceType.Daily;

  // Days of week options for weekly recurrence
  daysOfWeek = [
    { label: 'Sunday', value: 'SUN' },
    { label: 'Monday', value: 'MON' },
    { label: 'Tuesday', value: 'TUE' },
    { label: 'Wednesday', value: 'WED' },
    { label: 'Thursday', value: 'THU' },
    { label: 'Friday', value: 'FRI' },
    { label: 'Saturday', value: 'SAT' }
  ];

  // Common time zones
  timeZones = [
    'UTC',
    'America/New_York',
    'America/Chicago',
    'America/Denver',
    'America/Los_Angeles',
    'Europe/London',
    'Europe/Paris',
    'Europe/Madrid',
    'Asia/Tokyo',
    'Asia/Shanghai',
    'Australia/Sydney'
  ];

  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private scheduleService: ScheduleService,
    private route: ActivatedRoute,
    private router: Router,
    private message: NzMessageService
  ) {
    this.initializeForm();
  }

  ngOnInit(): void {
    this.recurrenceTypes = this.scheduleService.getRecurrenceTypes();

    // Check if editing existing schedule
    this.route.params.pipe(takeUntil(this.destroy$)).subscribe(params => {
      if (params['id']) {
        this.isEditMode = true;
        this.scheduleId = parseInt(params['id']);
        this.loadSchedule();
      }
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /**
   * Initialize form with all controls
   */
  private initializeForm(): void {
    this.form = this.fb.group({
      // Schedule Event fields
      name: ['', [Validators.required, Validators.maxLength(256)]],
      description: ['', [Validators.maxLength(1024)]],
      ruleId: ['', Validators.required],
      isActive: [true],
      endpointOverride: ['', [Validators.maxLength(512), this.urlValidator]],

      // Schedule Config fields
      recurrenceType: [RecurrenceType.Daily, Validators.required],
      startDate: [null, Validators.required],
      endDate: [null],
      timeOfDay: [''],
      daysOfWeek: [[]],
      dayOfMonth: [1],
      intervalValue: [1],
      cronExpression: [''],
      timeZone: ['UTC']
    });

    // Subscribe to recurrence type changes
    this.form.get('recurrenceType')?.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe(type => {
        this.currentRecurrenceType = type;
        this.updateValidators();
      });
  }

  /**
   * Update validators based on recurrence type
   */
  private updateValidators(): void {
    const timeOfDay = this.form.get('timeOfDay');
    const daysOfWeek = this.form.get('daysOfWeek');
    const dayOfMonth = this.form.get('dayOfMonth');
    const intervalValue = this.form.get('intervalValue');
    const cronExpression = this.form.get('cronExpression');

    // Clear validators
    timeOfDay?.clearValidators();
    daysOfWeek?.clearValidators();
    dayOfMonth?.clearValidators();
    intervalValue?.clearValidators();
    cronExpression?.clearValidators();

    // Set validators based on recurrence type
    switch (this.currentRecurrenceType) {
      case RecurrenceType.Once:
        timeOfDay?.setValidators([Validators.required]);
        break;
      case RecurrenceType.Minutely:
      case RecurrenceType.Hourly:
        intervalValue?.setValidators([Validators.required, Validators.min(1), Validators.max(1440)]);
        break;
      case RecurrenceType.Daily:
        timeOfDay?.setValidators([Validators.required]);
        break;
      case RecurrenceType.Weekly:
        timeOfDay?.setValidators([Validators.required]);
        daysOfWeek?.setValidators([Validators.required]);
        break;
      case RecurrenceType.Monthly:
        timeOfDay?.setValidators([Validators.required]);
        dayOfMonth?.setValidators([Validators.required, Validators.min(1), Validators.max(31)]);
        break;
      case RecurrenceType.CronExpression:
        cronExpression?.setValidators([Validators.required]);
        break;
    }

    timeOfDay?.updateValueAndValidity();
    daysOfWeek?.updateValueAndValidity();
    dayOfMonth?.updateValueAndValidity();
    intervalValue?.updateValueAndValidity();
    cronExpression?.updateValueAndValidity();
  }

  /**
   * Load schedule data for editing
   */
  private loadSchedule(): void {
    if (!this.scheduleId) return;

    this.loading = true;
    this.scheduleService.getScheduleEventById(this.scheduleId)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          this.populateForm(response);
          this.loading = false;
        },
        error: (error) => {
          this.message.error('Failed to load schedule');
          console.error('Error loading schedule:', error);
          this.loading = false;
        }
      });
  }

  /**
   * Populate form with schedule data
   */
  private populateForm(schedule: ScheduleEventModel): void {
    this.form.patchValue({
      name: schedule.name,
      description: schedule.description,
      ruleId: schedule.ruleId,
      isActive: schedule.isActive,
      endpointOverride: schedule.endpointOverride,
      recurrenceType: schedule.scheduleConfig.recurrenceType,
      startDate: this.toDate(schedule.scheduleConfig.startDate),
      endDate: this.toDate(schedule.scheduleConfig.endDate),
      timeOfDay: schedule.scheduleConfig.timeOfDay || '',
      daysOfWeek: schedule.scheduleConfig.daysOfWeek?.split(',') || [],
      dayOfMonth: schedule.scheduleConfig.dayOfMonth,
      intervalValue: schedule.scheduleConfig.intervalValue,
      cronExpression: schedule.scheduleConfig.cronExpression,
      timeZone: schedule.scheduleConfig.timeZone
    });

    this.currentRecurrenceType = schedule.scheduleConfig.recurrenceType;
    this.updateValidators();
  }

  /**
   * Handle form submission
   */
  onSubmit(): void {
    if (this.form.invalid) {
      this.message.error('Please fill all required fields correctly');
      this.markFormGroupTouched(this.form);
      return;
    }

    const formValue = this.form.value;
    const recurrenceType = formValue.recurrenceType as RecurrenceType;
    const schedule: ScheduleEventModel = {
      id: this.scheduleId || 0,
      name: formValue.name,
      description: formValue.description,
      ruleId: formValue.ruleId,
      isActive: formValue.isActive,
      endpointOverride: formValue.endpointOverride,
      scheduleConfig: {
        recurrenceType,
        startDate: this.toApiDate(formValue.startDate),
        endDate: this.toApiDate(formValue.endDate),
        timeOfDay: this.isFieldVisible('timeOfDay') ? formValue.timeOfDay : undefined,
        daysOfWeek: recurrenceType === RecurrenceType.Weekly
          ? (formValue.daysOfWeek as string[]).join(',')
          : undefined,
        dayOfMonth: recurrenceType === RecurrenceType.Monthly ? formValue.dayOfMonth : undefined,
        intervalValue: [RecurrenceType.Minutely, RecurrenceType.Hourly].includes(recurrenceType)
          ? formValue.intervalValue
          : undefined,
        cronExpression: recurrenceType === RecurrenceType.CronExpression ? formValue.cronExpression : undefined,
        timeZone: formValue.timeZone
      },
      createdAt: new Date().toISOString()
    };

    this.loading = true;

    const request$ = this.isEditMode
      ? this.scheduleService.updateScheduleEvent(this.scheduleId!, schedule)
      : this.scheduleService.createScheduleEvent(schedule);

    request$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response) => {
        this.message.success(
          this.isEditMode
            ? 'Schedule updated successfully'
            : 'Schedule created successfully'
        );
        this.router.navigate(['/zamba/schedule']);
        this.loading = false;
      },
      error: (error) => {
        this.message.error(this.isEditMode ? 'Failed to update schedule' : 'Failed to create schedule');
        console.error('Error saving schedule:', error);
        this.loading = false;
      }
    });
  }

  /**
   * Navigate back to schedule list
   */
  onCancel(): void {
    this.router.navigate(['/zamba/schedule']);
  }

  toggleDay(day: string, checked: boolean): void {
    const control = this.form.get('daysOfWeek');
    const selectedDays = (control?.value as string[]) || [];
    control?.setValue(checked
      ? [...selectedDays, day]
      : selectedDays.filter(selectedDay => selectedDay !== day));
    control?.markAsTouched();
  }

  /**
   * Get form control
   */
  getControl(name: string) {
    return this.form.get(name);
  }

  /**
   * Get error message for form field
   */
  getErrorMessage(fieldName: string): string {
    const control = this.form.get(fieldName);
    if (!control || !control.errors) return '';

    if (control.errors['required']) return `${fieldName} is required`;
    if (control.errors['maxlength']) {
      return `${fieldName} cannot exceed ${control.errors['maxlength'].requiredLength} characters`;
    }
    if (control.errors['invalidUrl']) return 'Please enter a valid URL';
    if (control.errors['min']) return `${fieldName} must be at least ${control.errors['min'].min}`;
    if (control.errors['max']) return `${fieldName} cannot exceed ${control.errors['max'].max}`;

    return 'Invalid input';
  }

  /**
   * Custom URL validator
   */
  private urlValidator(control: any) {
    if (!control.value) return null;

    try {
      new URL(control.value);
      return null;
    } catch {
      return { invalidUrl: true };
    }
  }

  private toDate(value?: string | null): Date | null {
    if (!value) return null;

    const date = new Date(value);
    return Number.isNaN(date.getTime()) ? null : date;
  }

  private toApiDate(value?: Date | null): string | undefined {
    if (!value) return undefined;

    return new Date(Date.UTC(value.getFullYear(), value.getMonth(), value.getDate())).toISOString();
  }

  /**
   * Mark all fields as touched for validation display
   */
  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
    });
  }

  /**
   * Show/hide specific fields based on recurrence type
   */
  isFieldVisible(fieldName: string): boolean {
    switch (fieldName) {
      case 'timeOfDay':
        return [RecurrenceType.Once, RecurrenceType.Daily, RecurrenceType.Weekly, RecurrenceType.Monthly].includes(this.currentRecurrenceType);
      case 'daysOfWeek':
        return this.currentRecurrenceType === RecurrenceType.Weekly;
      case 'dayOfMonth':
        return this.currentRecurrenceType === RecurrenceType.Monthly;
      case 'intervalValue':
        return [RecurrenceType.Minutely, RecurrenceType.Hourly].includes(this.currentRecurrenceType);
      case 'cronExpression':
        return this.currentRecurrenceType === RecurrenceType.CronExpression;
      case 'endDate':
        return this.currentRecurrenceType !== RecurrenceType.Once;
      default:
        return true;
    }
  }
}
