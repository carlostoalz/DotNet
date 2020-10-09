import { IError } from './IError';

export interface IResultado {
    datos: any;
    exitoso: boolean;
    catidadPaginas: number;
    error: IError;
    codigoRespuesta: number;
}