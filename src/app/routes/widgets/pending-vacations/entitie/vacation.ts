export class Vacation {
  // Properties
  public AuthorizeOption: string = '';
  public VacationFromOption: Date = new Date();
  public VacationToOption: Date = new Date();
  public RequestedDaysOption: string = '';
  public TotalDays: string = '';

  public DocType: string = "";
  public docid: string = "";
  public taskid: string = "";
  public mode: string = "";
  public s: string = "";
  public userId: string = "";


  // Methods
  constructor() { }
  public getDuration(): number {
    const duration = this.VacationToOption.getTime() - this.VacationFromOption.getTime();
    return Math.floor(duration / (1000 * 60 * 60 * 24));
  }
}
