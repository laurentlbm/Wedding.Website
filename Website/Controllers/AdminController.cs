using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Website.Models;
using Website.Services;

namespace Website.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly InvitationRepository _repository = new InvitationRepository();
        private readonly InvitationMailer _mailer = new InvitationMailer();

        public ViewResult Index()
        {
            var invitations = _repository.GetAll();

            return View(invitations);
        }

        public async Task<ActionResult> Send(Guid id)
        {
            var invitation = GetInvitationOrThrowHttpException(id);

            await SendInvitationAsync(invitation);

            return RedirectToAction("Index");
        }

        private async Task SendInvitationAsync(Invitation invitation)
        {
            await _mailer.SendInvitationAsync(invitation);

            _repository.MarkAsSent(invitation.Id);
        }

        public async Task<ActionResult> SendConfirmation(Guid id)
        {
            var invitation = GetInvitationOrThrowHttpException(id);

            await _mailer.SendConfirmationAsync(invitation);

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Invitation invitation, bool sendInvite = false)
        {
            if (!ModelState.IsValid)
            {
                return View(invitation);
            }

            invitation.Id = _repository.Create(invitation);

            if (sendInvite)
            {
                await SendInvitationAsync(invitation);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id, bool success = false)
        {
            var invitation = GetInvitationOrThrowHttpException(id);

            if (success)
            {
                ViewBag.SuccessMessage = "Invitation saved.";
            }

            return View(invitation);
        }

        [HttpPost]
        public ActionResult Edit(Invitation invitation)
        {
            if (!ModelState.IsValid)
            {
                return View(invitation);
            }

            var original = GetInvitationOrThrowHttpException(invitation.Id);

            // Values that aren't modified.
            invitation.InvitationSentAt = original.InvitationSentAt;
            invitation.LastViewedAt = original.LastViewedAt;

            _repository.Update(invitation);

            return RedirectToAction("Edit", new {id = invitation.Id, success = true});
        }

        private Invitation GetInvitationOrThrowHttpException(Guid id)
        {
            if (id == default(Guid))
            {
                throw new HttpException((int) HttpStatusCode.BadRequest, "Invalid invitation code.");
            }

            var invitation = _repository.Get(id);
            if (invitation == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "No such invitation.");
            }

            return invitation;
        }
    }
}
