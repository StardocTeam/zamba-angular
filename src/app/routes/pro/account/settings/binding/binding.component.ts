import { ChangeDetectionStrategy, Component } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-account-settings-binding',
  templateUrl: './binding.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProAccountSettingsBindingComponent {
  constructor(public msg: NzMessageService) {}

  Family: any = {
    CuitEmpresa: '',
    Legajo: '',
    ApellidoNombre: '',
    CuilCuit: '',
    Atributo: '',
    NivelEstudio: '',
    EstadoEstudioNivelEstudio: '',
    InstitucionEducativa: '',
    EstadoEstudioInstitucion: '',
    OtraInstitucion: '',
    TituloCertificacion: '',
    FechaTitulacion: '',
    Observaciones: 'Aca va un comentario ... Escriba aqui... esto es un TEST.',
    piso: 1,
  };

  Educacion: any = {
    Nombre: '',
    Apellido: '',
    CuilCuit: 0,
    Nacionalidad: '',
    TipoDocumento: '',
    NumeroDocumento: 0,
  };
}
