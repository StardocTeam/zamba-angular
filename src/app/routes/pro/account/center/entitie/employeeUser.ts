export class employeeUser {
  avatar: string;
  name: string;
  lastName: string;
  workEmail: string;
  workCellPhone: string;
  birthday: Date;

  password: string;

  employmentStatus: string;
  area: string;
  department: string;
  position: string;

  workMode: string;

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
    this.workEmail = '';

    this.workCellPhone = '';
    this.workMode = '';

    this.birthday = new Date();
    this.dateEmploymentEntry = new Date();

    /*----*/

    this.company = '';
  }
}
