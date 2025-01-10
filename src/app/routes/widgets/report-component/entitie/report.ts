export class Report {
    Aditional: number;
    Completar: string;
    GroupExpression: any;
    ID: number;
    Name: string;
    OBJECTID: number;
    Query: string;
    RIGHT_TYPE: number;
    USER_ID: number;
    USER_NAME: string;

    constructor(data: any) {
        this.Aditional = data.Aditional;
        this.Completar = data.Completar;
        this.GroupExpression = data.GroupExpression;
        this.ID = data.ID;
        this.Name = data.Name;
        this.OBJECTID = data.OBJECTID;
        this.Query = data.Query;
        this.RIGHT_TYPE = data.RIGHT_TYPE;
        this.USER_ID = data.USER_ID;
        this.USER_NAME = data.USER_NAME;
    }
}