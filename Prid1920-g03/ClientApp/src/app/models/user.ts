export class User {
    pseudo: string;
    password: string;
    lastName: string;
    firstName: string;
    birthDate: string;
    reputation: number;
    constructor(data: any) {
      if (data) {
        this.pseudo = data.pseudo;
        this.password = data.password;
        this.lastName = data.lastName;
        this.firstName = data.firstName;
        this.birthDate = data.birthDate &&
          data.birthDate.length > 10 ? data.birthDate.substring(0, 10) : data.birthDate;
        this.reputation = data.reputation;  
      }
    }
  }