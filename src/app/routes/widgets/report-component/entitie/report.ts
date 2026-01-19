export class Report {
    ID: number = 0;
    Name: string = "";
    Category: string = ""; //TODO: convertir esto de string a Category, y que esta el uso de esta variable sea Category.name en lugar de Category.id
    Query: string = "";
    Description: string = "";
    Aditional: number = 0;
    Completar: string = "";
    GroupExpression: string = "";
    RuleId: string | null = null;

    constructor(data: any) {
        this.Aditional = data.Aditional;
        this.Completar = data.Completar;
        this.ID = data.ID;
        this.Name = data.Name;
        this.Query = data.Query;
        this.Category = data.Category;
        this.Description = data.Description;
        this.RuleId = data.RuleId;
    }
}