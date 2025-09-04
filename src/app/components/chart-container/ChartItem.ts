// Clase tipada para los elementos de chartList
export class ChartItem {
    constructor(
        public Id: number,
        public PosY: number,
        public PosX: number,
        public DimYSpan: number,
        public DimXSpan: number,
        public ChartTypeDescripcion: string,
        public AttrSelected: string
    ) { }
}
