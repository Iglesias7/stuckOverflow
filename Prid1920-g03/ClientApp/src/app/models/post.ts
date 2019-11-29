import { Vote } from "./vote";
import { Tag } from "./Tag";

  export class Post {
      id: number;
      title: string;
      body: string;
      timestamp: string;
      authorId: number;
      parentId: number;
      acceptedAnswerId: number;
      
      comments: any[];
      responses: (string | Post)[];
      tags: any[];
      votes: (number | Vote)[];
    
      constructor(data: any) {
        if (data) {
          this.id = data.id;
          this.title = data.title;
          this.body = data.body;
          this.timestamp = data.timestamp;
          
          this.authorId = data.AuthorId;
          this.parentId = data.parentId;
          this.acceptedAnswerId = data.acceptedAnswerId;          
  
          this.comments = data.Comments;
          this.responses = data.Replies;
          this.tags = data.LsTags;
          this.votes = data.Votes;
        }
      }
    }  
  
  