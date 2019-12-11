
  export class Tag {
    id: number;
    name: string;
    isChecked: boolean;
    
    constructor(data: any) {
      if(data) {
        this.id = data.id;
        this.name = data.name;
        this.isChecked = false;
      }
    }
  }

   