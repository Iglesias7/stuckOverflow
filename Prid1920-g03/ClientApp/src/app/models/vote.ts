
export class Vote {
  updown: number; 
  authorId: number;
  postId: number;

  constructor(data: any) {
    this.updown = data.upDown;
    this.authorId = data.authorId;
    this.postId = data.postId;
  }
}


