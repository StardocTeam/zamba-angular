// Clase tipada para los elementos de chartList
export class ChartItem {
    constructor(
        public Id: number,
        public PosY: number,
        public PosX: number,
        public ChartTypeDescripcion: string,
        public AttrSelected: string,
        public ChartTypeId: number,
        public ChartTitle: string,
        public DimX: number,
        public DimY: number,
    ) { }
}
