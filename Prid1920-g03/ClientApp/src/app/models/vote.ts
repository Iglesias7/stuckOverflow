
export class Vote {
  updown: number; 
  authorId: number;
  postId: number;

  constructor(data: any) {
    this.updown = 1 || -1;
    this.authorId = data.authorId;
    this.postId = data.postId;
  }
}


