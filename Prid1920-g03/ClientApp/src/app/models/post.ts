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

      user: any;
      postParent: any;
      numResponse: number;
      numVote: number;
      numComments: number;
      voteState: number;
      hightVote: number;
      
      comments: any[];
      responses: (string | Post)[];
      tags: string[];
      votes: Vote[];
    
      constructor(data: any) {
        if (data) {
          this.id = data.id;
          this.title = data.title;
          this.body = data.body;
          this.timestamp = data.timestamp;
          
          this.authorId = data.authorId;
          this.parentId = data.parentId;
          this.acceptedAnswerId = data.acceptedAnswerId;          
          
          this.user = data.user;
          this.numResponse = data.numResponse,
          this.hightVote = data.hightVote,
          this.voteState = data.voteState,
          this.numVote = data.numVote,
          this.numComments = data.numComments,

          this.comments = data.comments;
          this.responses = data.replies;
          this.tags = data.tags;
          this.votes = data.votes;
        }
      }
    }  
  
  