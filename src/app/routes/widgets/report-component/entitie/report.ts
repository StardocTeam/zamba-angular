export class Report {
    Aditional: number;
    Completar: string;
    ID: number;
    Name: string;
    Query: string;
    Category: string;
    Description: string

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