import { Component } from '@angular/core';

@Component({
  selector: 'app-empleado',
  templateUrl: './empleado.component.html',
  styleUrls: ['./empleado.component.less'],
  styles: [
    `
      nz-select {
        margin: 0 8px 10px 0;
        width: 100%;
      }
    `,
  ],
})
export class EmpleadoComponent {
  Family: any = {
    CuitEmpresa: 0,
    Legajo: '',
    ApellidoNombre: '',
    CuilCuit: '',
    Nombre: '',
    Apellido: '',
    FechaNacimiento: Date.now(),
    Nacionalidad: '',
    TipoDocumento: '',
    NumeroDocumento: '',
    Genero: 'Male',
    EstadoCivil: 'Single',
    Pais: 'Argentina',
    Provincia: 'Buenos Aires',
    Localidad: '',
    Barrio: '',
    Municipio: '',
    DepartamentoProvincial: '',
    Calle: '',
    Altura: 0,
    BeneficioSeguroDeVida: '',
    PersonaACargo: '',
    ProcentajeDeduccionGanancia: 0,
  };
}
