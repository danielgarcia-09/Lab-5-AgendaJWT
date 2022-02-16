using AgendaManager.Model.Enum;

namespace AgendaManager.Bl.Dto
{
    public class InvitationDto : BaseDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int @EventId { get; set; }
        public InvitationStatus Status { get; set; }
    }
}
