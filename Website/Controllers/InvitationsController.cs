using System;
using System.Web.Mvc;
using Website.Localization;
using Website.Models;
using Website.Services;

namespace Website.Controllers
{
    public class InvitationsController : Controller
    {
        private readonly InvitationRepository _repository = new InvitationRepository();
        private readonly InvitationMailer _mailer = new InvitationMailer();

        public ActionResult Index(Guid id, bool? success = null)
        {
            var invitation = _repository.Get(id);

            if (invitation == null)
            {
                return InvalidInvitationCode();
            }

            _repository.MarkAsViewed(id);

            if (success == true)
            {
                ViewBag.SuccessMessage = Language.Saved;
            }

            return View(invitation);
        }

        [HttpPost]
        public ActionResult Index(Invitation input)
        {
            var invitation = _repository.Get(input.Id);

            if (invitation == null)
            {
                return InvalidInvitationCode();
            }

            invitation.IsAttending = input.IsAttending.GetValueOrDefault();
            invitation.Guests = invitation.IsAttending == true ? Math.Min(input.Guests.GetValueOrDefault(1), invitation.AllowedGuests) : 0;
            invitation.Note = input.Note;

            _repository.Update(invitation);

            // Don't await, send emails in the background
            _mailer.SendConfirmationAsync(invitation);
            _mailer.NotifyLaurentAsync(invitation);

            return RedirectToAction("Index", new { success = true });
        }

        public ViewResult Demo()
        {
            var invitation = new Invitation
                {
                    FullName = "Julien & Andrea",
                    AllowedGuests = 3
                };

            return View("Index", invitation);
        }

        [HttpPost]
        public RedirectToRouteResult Check(Guid? id)
        {
            if (id == null || id.Value == default(Guid))
            {
                return InvalidInvitationCode();
            }

            var invite = _repository.Get(id.Value);
            if (invite == null)
            {
                return InvalidInvitationCode();
            }

            return RedirectToAction("Index", new { id });
        }

        private RedirectToRouteResult InvalidInvitationCode()
        {
            return RedirectToAction("Index", "Home", new { error = 1 });
        }
    }
}
