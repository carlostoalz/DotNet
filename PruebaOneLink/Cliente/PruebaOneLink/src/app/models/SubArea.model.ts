export class SubArea {
    constructor(
        public idSubArea: number,
        public nombreSubArea: string,
        public idArea: number,
        public usuarioCreacion: number,
        public fechaCreacion?: Date,
        public usuarioModificacion?: number,
        public fechaModificacion?: Date
    ) {
        
    }
}