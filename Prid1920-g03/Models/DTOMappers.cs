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

                Posts = user.Posts.ToDTO(),
                Comments = user.Comments.ToDTO(),
                Votes = user.Votes.ToDTO()
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

                Posts = post.Posts.ToDTO(),
                Comments = post.Comments.ToDTO(),
                Votes = post.Votes.ToDTO(),
                // Tags = post.Tags.ToDTO()
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
            };
        }

        public static List<CommentDTO> ToDTO(this IEnumerable<Comment> comments) {
            return comments.Select(c => c.ToDTO()).ToList();
        }
//----------------------------VoteDTO-----------------------------------------------------
        public static VoteDTO ToDTO(this Vote vote) {
            return new VoteDTO {
                PostId = vote.PostId,
                UserId = vote.UserId,
                UpDown = vote.UpDown,
                Timestamp = vote.Timestamp,
            };
        }

        public static List<VoteDTO> ToDTO(this IEnumerable<Vote> votes) {
            return votes.Select(v => v.ToDTO()).ToList();
        }

//-----------------------TagDTO--------------------------------------------------------
        // public static TagDTO ToDTO(this Tag tag) {
        //     return new TagDTO {
        //         Id = tag.Id,
        //         Name = tag.Name,

        //         Posts = tag.Posts.ToDTO(),
        //     };
        // }

        // public static List<TagDTO> ToDTO(this IEnumerable<Tag> tags) {
        //     return tags.Select(t => t.ToDTO()).ToList();
        // }
    }
}