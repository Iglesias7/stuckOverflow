using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prid1920_g03.Models
{
    public static class DTOMappers {

//----------------------------UserDTO-------------------------------------------------------
        public static UserDTO ToDTO(this User user) {
            return new UserDTO {
                Id = user.Id,
                Pseudo = user.Pseudo,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Reputation = user.Reputation,
                PicturePath = user.PicturePath,
                Role = user.Role,
                Token = user.Token,


            };
        }

        public static List<UserDTO> ToDTO(this IEnumerable<User> users) {
            return users.Select(u => u.ToDTO()).ToList();
        }

//---------------------------PostDTO----------------------------------------------------------------
        public static PostDTO ToDTO(this Post post) {
            return new PostDTO {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Timestamp = post.Timestamp,
                // AcceptedAnswerIdExist = post.AcceptedAnswerIdExist,

                AuthorId = post.AuthorId,
                AcceptedAnswerId = post.AcceptedAnswerId,
                ParentId = post.ParentId,
                NumResponse = post.NumResponse,
                VoteState = post.VoteState,
                NumVote = post.NumVote,
                HightVote = post.HightVote,
                NumComments = post.NumComment,
                User = post.User.ToDTOPU(),
                
                Replies = post.Responses.ToDTO(),
                Comments = post.Comments.ToDTO(),
                Votes = post.Votes.ToDTO(),
                Tags = post.Tags.Select(t => t.Name).ToList()
            };
        }

        public static PostUser ToDTOPU(this User user) {
            return new PostUser {
                Id = user.Id,
                Pseudo = user.Pseudo,
                Reputation = user.Reputation,
                PicturePath = user.PicturePath,
                Role = user.Role
            };
        }

        public static List<PostDTO> ToDTO(this IEnumerable<Post> posts) {
            return posts.Select(p => p.ToDTO()).ToList();
        }
//------------------------CommentDTO--------------------------------------------------
        public static CommentDTO ToDTO(this Comment comment) {
            return new CommentDTO {
                Id = comment.Id,
                Body = comment.Body,
                Timestamp = comment.Timestamp,
                CommentUser = comment.User.ToDTOPU(),

                PostId = comment.PostId,
                AuthorId = comment.AuthorId
            };
        }

        public static List<CommentDTO> ToDTO(this IEnumerable<Comment> comments) {
            return comments.Select(c => c.ToDTO()).ToList();
        }
//----------------------------VoteDTO-----------------------------------------------------
        public static VoteDTO ToDTO(this Vote vote) {
            return new VoteDTO {
                UpDown = vote.UpDown,

                AuthorId = vote.AuthorId,
                PostId = vote.PostId,

            };
        }

        public static List<VoteDTO> ToDTO(this IEnumerable<Vote> votes) {
            return votes.Select(v => v.ToDTO()).ToList();
        }

//-----------------------TagDTO--------------------------------------------------------
         public static TagDTO ToDTO(this Tag tag) {
             return new TagDTO {
                 Id = tag.Id,
                 Name = tag.Name,
                 NbXPosts = tag.NbXPosts,
                 Body = tag.Body,
                 Timestamp = tag.Timestamp,
             };
         }

         public static List<TagDTO> ToDTO(this IEnumerable<Tag> tags) {
             return tags.Select(t => t.ToDTO()).ToList();
         }
    }
}