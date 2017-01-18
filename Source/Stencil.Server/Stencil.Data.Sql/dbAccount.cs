//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stencil.Data.Sql
{
    using System;
    using System.Collections.Generic;
    
    public partial class dbAccount
    {
        public System.Guid account_id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string password_salt { get; set; }
        public bool disabled { get; set; }
        public string api_key { get; set; }
        public string api_secret { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string entitlements { get; set; }
        public Nullable<System.DateTimeOffset> last_login_utc { get; set; }
        public string last_login_platform { get; set; }
        public System.DateTimeOffset created_utc { get; set; }
        public System.DateTimeOffset updated_utc { get; set; }
        public Nullable<System.DateTimeOffset> deleted_utc { get; set; }
        public Nullable<System.DateTimeOffset> sync_hydrate_utc { get; set; }
        public Nullable<System.DateTimeOffset> sync_success_utc { get; set; }
        public Nullable<System.DateTimeOffset> sync_invalid_utc { get; set; }
        public Nullable<System.DateTimeOffset> sync_attempt_utc { get; set; }
        public string sync_agent { get; set; }
        public string sync_log { get; set; }
        public string password_reset_token { get; set; }
        public Nullable<System.DateTimeOffset> password_reset_utc { get; set; }
        public string push_ios { get; set; }
        public string push_google { get; set; }
        public string push_microsoft { get; set; }
    }
}
