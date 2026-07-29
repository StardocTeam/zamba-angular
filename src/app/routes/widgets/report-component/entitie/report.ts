export class Report {
  ID: number = 0;
  Name: string = '';
  Categorydescription: string = '';
  Categoryid: number = 0;
  Query: string = '';
  Description: string = '';
  Aditional: number = 0;
  Completar: string = '';
  GroupExpression: string = '';
  RuleId: string | null = null;

  constructor(data: any) {
    this.Aditional = parseInt(data.Aditional, 10);
    this.Completar = data.Completar;
    this.ID = parseInt(data.ID, 10);
    this.Name = data.Name;
    this.Query = data.Query;
    this.Categorydescription = data.Category;
    this.Categoryid = parseInt(data.Categoryid, 10);
    this.Description = data.Description;
    this.RuleId = data.RuleId;
  }
}
