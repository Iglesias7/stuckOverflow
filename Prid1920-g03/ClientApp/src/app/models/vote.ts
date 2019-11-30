
export class Vote {
  id: number;
  updown: number; 
  authorId: number;
  postId: number;

  constructor(data: any) {
    this.id = data.id;
    this.updown = 1 || -1;
    this.authorId = data.authorId;
    this.postId = data.postId;
  }
}


