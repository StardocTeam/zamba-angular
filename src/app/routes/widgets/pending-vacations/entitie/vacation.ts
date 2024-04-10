export class Vacation {
  // Properties
  public AuthorizeOption: string = '';
  public VacationFromOption: Date = new Date();
  public VacationToOption: Date = new Date();
  public RequestedDaysOption: string = '';
  public TotalDays: string = '';

  // Methods
  constructor() {}
  public getDuration(): number {
    const duration = this.VacationToOption.getTime() - this.VacationFromOption.getTime();
    return Math.floor(duration / (1000 * 60 * 60 * 24));
  }
}
