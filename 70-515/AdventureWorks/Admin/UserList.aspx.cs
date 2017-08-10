using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdventureWorks.Admin
{
	public partial class UserList : Page
	{
		private MembershipUserCollection _users;

		protected void Page_Load(object sender, EventArgs e)
		{
			_users = Membership.GetAllUsers();
			grdUsers.DataSource = _users;

			if (!IsPostBack)
			{
				grdUsers.DataBind();
			}
		}

		protected void grdUsers_SelectedIndexChanged(object sender, EventArgs e)
		{
			MembershipUser user = _users[Convert.ToString(grdUsers.SelectedValue)];
			if (user != null)
			{
				pnlUserInfo.Visible = true;
				lblLastLoginOn.Text = user.LastLoginDate.ToString();
				lblPasswordQuestion.Text = user.PasswordQuestion;
				lblUsername.Text = user.UserName;
				txtComment.Text = user.Comment;
				txtEmail.Text = user.Email;
				chkIsApproved.Checked = user.IsApproved;
				chkIsLockedOut.Checked = user.IsLockedOut;
			}
		}

		protected void btnUpdateUser_Click(object sender, EventArgs e)
		{
			string userName = Convert.ToString(grdUsers.SelectedValue);

			if (!String.IsNullOrEmpty(userName))
			{
				MembershipUser user = _users[userName];

				user.Comment = txtComment.Text;
				user.Email = txtEmail.Text;
				user.IsApproved = chkIsApproved.Checked;

				Membership.UpdateUser(user);

				pnlUserInfo.Visible = false;
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			pnlUserInfo.Visible = false;
		}
	}
}