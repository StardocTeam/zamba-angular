export class employeeUser {
  avatar: string;

  name: string;
  lastName: string;
  email: string;
  workCellPhone: string;
  birthday: Date;

  password: string;

  employmentStatus: string;
  area: string;
  department: string;
  position: string;

  workMode: string;

  //TODO: eliminar comentarios si no se usaran luego como variables.
  // workLocation: string;
  // country: string;
  // city: string;
  // state: string;
  // timeDifference: string;
  // workSchedule: string;
  // zip: number;

  company: string;
  dateEmploymentEntry: Date;

  /**
   *
   */
  constructor() {
    this.avatar = '';
    this.name = '';
    this.lastName = '';
    this.password = '';

    this.employmentStatus = '';
    this.area = '';
    this.department = '';
    this.position = '';
    this.email = '';

    this.workCellPhone = '';
    this.workMode = '';

    this.birthday = new Date();
    this.dateEmploymentEntry = new Date();

    /*----*/

    this.company = '';

    // this.workLocation = '';
    // this.timeDifference = '';
    // this.workSchedule = '';
    // this.zip = 0;
  }
}
