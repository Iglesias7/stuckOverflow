import { User } from "./user";

  export class Post {
      id: number;
      title: string;
      body: string;
      timestamp: string;
      authorId: number;
      parentId: number;
      acceptedAnswerId: number;

      user: any;
      
      comments: (string | Comment)[];
      responses: (string | Post)[];
      tags: string[];
      votes: number;
    
      constructor(data: any) {
        if (data) {
          this.id = data.id;
          this.title = data.title;
          this.body = data.body;
          this.timestamp = data.timestamp;
          
          this.authorId = data.authorId;
          this.parentId = data.parentId;
          this.acceptedAnswerId = data.acceptedAnswerId;          
          
          this.user = data.postUser;

          this.comments = data.comments;
          this.responses = data.replies;
          this.tags = data.lsTags;
          this.votes = data.votes;
        }
      }
    }  
  
  