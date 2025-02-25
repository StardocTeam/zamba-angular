export class Report {
    ID: number;
    Name: string;
    Category: string;
    Query: string;
    Description: string
    Aditional: number;
    Completar: string;

    constructor(data: any) {
        this.Aditional = data.Aditional;
        this.Completar = data.Completar;
        this.ID = data.ID;
        this.Name = data.Name;
        this.Query = data.Query;
        this.Category = data.Category;
        this.Description = data.Description;
    }
}