export class TipoDocumento {
    constructor(
        public idTipoDocumento: number,
        public nombreTipoDocumento: string,
        public usuarioCreacion: number,
        public fechaCreacion?: Date,
        public usuarioModificacion?: number,
        public fechaModificacion?: Date
    ) {}
}