export class Area {
    constructor(
        public idArea: number,
        public nombreArea: string,
        public usuarioCreacion: number,
        public fechaCreacion?: Date,
        public usuarioModificacion?: number,
        public fechaModificacion?: Date
    ) {
        
    }
}