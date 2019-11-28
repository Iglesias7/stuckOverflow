
  export class Comment {
    id: number;
    body: string;
    

      constructor(data: any) {
        if(data){
          this.id = data.id;
          this.body = data.body;
        }
      }
  }
    


