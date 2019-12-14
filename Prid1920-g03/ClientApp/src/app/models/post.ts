import { User } from "./user";
import { Vote } from "./vote";

  export class Post {
      id: number;
      title: string;
      body: string;
      timestamp: string;
      authorId: number;
      parentId: number;
      acceptedAnswerId: number;
      acceptedAnswerIdExist: boolean;

      postUser: any;
      numResponse: number;
      numVote: number;
      numComments: number;
      voteState: number;
      
      comments: (string | Comment)[];
      responses: (string | Post)[];
      tags: string[];
      votes: Vote[];
    
      constructor(data: any) {
        if (data) {
          this.id = data.id;
          this.title = data.title;
          this.body = data.body;
          this.timestamp = data.timestamp;
          this.acceptedAnswerIdExist = data.acceptedAnswerIdExist;
          
          this.authorId = data.authorId;
          this.parentId = data.parentId;
          this.acceptedAnswerId = data.acceptedAnswerId;          
          
          this.postUser = data.postUser;
          this.numResponse = data.numResponse,
          this.voteState = data.voteState,
          this.numVote = data.numVote,
          this.numComments = data.numComments,

          this.comments = data.comments;
          this.responses = data.replies;
          this.tags = data.lsTags;
          this.votes = data.votes;
        }
      }
    }  
  
  