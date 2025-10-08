using BeToff.BLL.Dto.Response;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Mapping
{
    public static class MessageResponseMapper
    {
        public static MessageResponseDto ToDto(Message message)
        {
            return new MessageResponseDto
            {
                Id = message.Id,
                Content = message.Content,
                Sender = message.SenderId,
                SendAt = message.SendAt,
                Status = message.Status.ToString(),
            };

        }
    }
}
