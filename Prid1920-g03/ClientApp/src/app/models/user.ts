export enum Role {
  Member = 0,
  Manager = 1,
  Admin = 2
}

export class User {
    id: number;
    pseudo: string;
    password: string;
    firstName: string;
    lastName: string;
    email: string;
    reputation: number;
    birthDate: string;
    picturePath: string;
    role: Role;
    token: string;

    followers: (number | User)[];
    followees: (number | User)[];

    constructor(data: any) {
      if (data) {
        this.id = data.id;
        this.pseudo = data.pseudo;
        this.password = data.password;
        this.email = data.email;
        this.firstName = data.firstName;
        this.lastName = data.lastName;
        this.reputation = data.reputation;
        this.birthDate = data.birthDate &&
          data.birthDate.length > 10 ? data.birthDate.substring(0, 10) : data.birthDate;
        this.picturePath = data.picturePath;
        this.role = data.role || Role.Member;
        this.token = data.token;

        this.followers = data.followers;
        this.followees = data.followees;
      }
    }
    public get roleAsString(): string {
      return Role[this.role];
    }
  }

  export interface IFriend  {
    id: number;
    pseudo: string;
    password: string;
    firstName: string;
    lastName: string;
    email: string;
    reputation: number;
    birthDate: string;
    picturePath: string;
    role: Role;
    token: string;
    relationship: string;
  }


