export class Todo {
    ID: string;
    PROVEEDOR: string;
    MONEDA: string;
    IMPORTE: string;
    TIPO_GASTO: string;
    CONCEPTO: string;
    AREA: string;
    CODIGOA: string;
    VENCIMIENTO: boolean;
    completed: boolean;
    starred: boolean;
    important: boolean;
    deleted: boolean;
    

    /**
     * Constructor
     *
     * @param todo
     */
    constructor(todo) {
        {
            this.ID = todo.ID;
            this.AREA = todo.AREA;
            this.CODIGOA = todo.CODIGOA;
            this.CONCEPTO = todo.CONCEPTO;
            this.IMPORTE = todo.IMPORTE;
            this.MONEDA = todo.MONEDA;
            this.PROVEEDOR = todo.PROVEEDOR;
            this.TIPO_GASTO = todo.TIPO_GASTO;
            this.VENCIMIENTO = todo.VENCIMIENTO;
            
        }
    }

    /**
     * Toggle star
     */
    toggleStar(): void {
        this.starred = !this.starred;
    }

    /**
     * Toggle important
     */
    toggleImportant(): void {
        this.important = !this.important;
    }

    /**
     * Toggle completed
     */
    toggleCompleted(): void {
        this.completed = !this.completed;
    }

    /**
     * Toggle deleted
     */
    toggleDeleted(): void {
        this.deleted = !this.deleted;
    }
}
