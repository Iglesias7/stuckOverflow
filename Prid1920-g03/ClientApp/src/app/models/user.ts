export class User {
    pseudo: string;
    password: string;
    firstName: string;
    lastName: string;
    email: string;
    reputation: number;
    birthDate: string;


    constructor(data: any) {
      if (data) {
        this.pseudo = data.pseudo;
        this.password = data.password;
        this.email = data.email;
        this.firstName = data.firstName;
        this.lastName = data.lastName;
        this.reputation = data.reputation;
        this.birthDate = data.birthDate &&
          data.birthDate.length > 10 ? data.birthDate.substring(0, 10) : data.birthDate;
      }
    }
  }