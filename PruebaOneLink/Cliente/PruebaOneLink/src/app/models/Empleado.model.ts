export class Empleado {
    constructor(
        public idEmmpleado: number,
        public idTipoDocumento: number,
        public numeroDocumento: string,
        public nombres: string,
        public apellidos: string,
        public idArea: number,
        public idSubArea: number,
        public UsuarioCreacion: number,
        public fechaCreacion?: Date,
        public nombreTipoDocumento?: string,
        public nombreArea?: string,
        public nombreSubArea?: string
    ) {}
}