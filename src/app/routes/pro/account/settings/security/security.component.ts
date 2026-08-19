import { ChangeDetectionStrategy, Component } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-account-settings-security',
  templateUrl: './security.component.html',
  styles: [
    `
      nz-select {
        margin: 0 8px 10px 0;
        width: 100%;
      }
    `,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProAccountSettingsSecurityComponent {
  constructor(public msg: NzMessageService) {}

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

  DocFamily: any = {
    CuitEmpresa: '',
    Legajo: '',
    ApellidoNombre: '',
    CuilCuit: '',
    Parentezco: '',
    Apellido: '',
    Nombre: '',
    TipoDocumento: '',
    NumeroDocumento: '',
    DocFamiliares: '',
    Observaciones: '',
  };
}
