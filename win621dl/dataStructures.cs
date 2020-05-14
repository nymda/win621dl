using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace win621dl
{
    class dataStructures
    {

    }

    //e621 set
    public class File
    {
        public int width { get; set; }
        public int height { get; set; }
        public string ext { get; set; }
        public int size { get; set; }
        public string md5 { get; set; }
        public string url { get; set; }
    }

    public class Preview
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Sample
    {
        public bool has { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public string url { get; set; }
    }

    public class Score
    {
        public int up { get; set; }
        public int down { get; set; }
        public int total { get; set; }
    }

    public class Tags
    {
        public List<string> general { get; set; }
        public List<string> species { get; set; }
        public List<object> character { get; set; }
        public List<object> copyright { get; set; }
        public List<object> artist { get; set; }
        public List<object> invalid { get; set; }
        public List<object> lore { get; set; }
        public List<object> meta { get; set; }
    }

    public class Flags
    {
        public bool pending { get; set; }
        public bool flagged { get; set; }
        public bool note_locked { get; set; }
        public bool status_locked { get; set; }
        public bool rating_locked { get; set; }
        public bool deleted { get; set; }
    }

    public class Relationships
    {
        public int? parent_id { get; set; }
        public bool has_children { get; set; }
        public bool has_active_children { get; set; }
        public List<object> children { get; set; }
    }

    public class Post
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public File file { get; set; }
        public Preview preview { get; set; }
        public Sample sample { get; set; }
        public Score score { get; set; }
        public Tags tags { get; set; }
        public List<object> locked_tags { get; set; }
        public int change_seq { get; set; }
        public Flags flags { get; set; }
        public string rating { get; set; }
        public int fav_count { get; set; }
        public List<object> sources { get; set; }
        public List<object> pools { get; set; }
        public Relationships relationships { get; set; }
        public int? approver_id { get; set; }
        public int uploader_id { get; set; }
        public string description { get; set; }
        public int comment_count { get; set; }
        public bool is_favorited { get; set; }
    }

    public class RootObject
    {
        public List<Post> posts { get; set; }
    }

    //inkbunny set

    public enum dlSetting
    {
        Gallery,
        Tags
    }
    public partial class sessionData
    {
        public string sid { get; set; }
        public int user_id { get; set; }
        public int ratingsmask { get; set; }
    }

    public class RootobjectIB
    {
        public string sid { get; set; }
        public string results_count_all { get; set; }
        public int results_count_thispage { get; set; }
        public int pages_count { get; set; }
        public int page { get; set; }
        public string user_location { get; set; }
        public object[] search_params { get; set; }
        public Submission[] submissions { get; set; }
    }

    public class Submission
    {
        public string submission_id { get; set; }
        public string hidden { get; set; }
        public string username { get; set; }
        public string user_id { get; set; }
        public string create_datetime { get; set; }
        public string create_datetime_usertime { get; set; }
        public string last_file_update_datetime { get; set; }
        public string last_file_update_datetime_usertime { get; set; }
        public string thumbnail_url_huge { get; set; }
        public string thumbnail_url_large { get; set; }
        public string thumbnail_url_medium { get; set; }
        public string thumb_huge_x { get; set; }
        public string thumb_huge_y { get; set; }
        public string thumb_large_x { get; set; }
        public string thumb_large_y { get; set; }
        public string thumb_medium_x { get; set; }
        public string thumb_medium_y { get; set; }
        public string thumbnail_url_huge_noncustom { get; set; }
        public string thumbnail_url_large_noncustom { get; set; }
        public string thumbnail_url_medium_noncustom { get; set; }
        public string thumb_medium_noncustom_x { get; set; }
        public string thumb_medium_noncustom_y { get; set; }
        public string thumb_large_noncustom_x { get; set; }
        public string thumb_large_noncustom_y { get; set; }
        public string thumb_huge_noncustom_x { get; set; }
        public string thumb_huge_noncustom_y { get; set; }
        public string file_name { get; set; }
        public string title { get; set; }
        public string deleted { get; set; }
        public string _public { get; set; }
        public string mimetype { get; set; }
        public string pagecount { get; set; }
        public string rating_id { get; set; }
        public string rating_name { get; set; }
        public string file_url_full { get; set; }
        public string file_url_screen { get; set; }
        public string file_url_preview { get; set; }
        public string submission_type_id { get; set; }
        public string type_name { get; set; }
        public string digitalsales { get; set; }
        public string printsales { get; set; }
        public string friends_only { get; set; }
        public string guest_block { get; set; }
        public string scraps { get; set; }
        public string latest_file_name { get; set; }
        public string latest_mimetype { get; set; }
        public string latest_thumbnail_url_huge_noncustom { get; set; }
        public string latest_thumbnail_url_large_noncustom { get; set; }
        public string latest_thumbnail_url_medium_noncustom { get; set; }
        public string latest_thumb_medium_noncustom_x { get; set; }
        public string latest_thumb_medium_noncustom_y { get; set; }
        public string latest_thumb_large_noncustom_x { get; set; }
        public string latest_thumb_large_noncustom_y { get; set; }
        public string latest_thumb_huge_noncustom_x { get; set; }
        public string latest_thumb_huge_noncustom_y { get; set; }
    }

    public class RootobjectPID
    {
        public string sid { get; set; }
        public int results_count { get; set; }
        public string user_location { get; set; }
        public SubmissionPID[] submissions { get; set; }
    }

    public class SubmissionPID
    {
        public string submission_id { get; set; }
        public Keyword[] keywords { get; set; }
        public string hidden { get; set; }
        public string scraps { get; set; }
        public string favorite { get; set; }
        public string favorites_count { get; set; }
        public string create_datetime { get; set; }
        public string create_datetime_usertime { get; set; }
        public string last_file_update_datetime { get; set; }
        public string last_file_update_datetime_usertime { get; set; }
        public string username { get; set; }
        public string user_id { get; set; }
        public string user_icon_file_name { get; set; }
        public string user_icon_url_large { get; set; }
        public string user_icon_url_medium { get; set; }
        public string user_icon_url_small { get; set; }
        public string file_name { get; set; }
        public string file_url_full { get; set; }
        public string file_url_screen { get; set; }
        public string file_url_preview { get; set; }
        public string thumbnail_url_huge_noncustom { get; set; }
        public string thumbnail_url_large_noncustom { get; set; }
        public string thumbnail_url_medium_noncustom { get; set; }
        public string thumb_medium_noncustom_x { get; set; }
        public string thumb_medium_noncustom_y { get; set; }
        public string thumb_large_noncustom_x { get; set; }
        public string thumb_large_noncustom_y { get; set; }
        public string thumb_huge_noncustom_x { get; set; }
        public string thumb_huge_noncustom_y { get; set; }
        public string watching { get; set; }
        public FilePID[] files { get; set; }
        public object[] pools { get; set; }
        public int pools_count { get; set; }
        public string title { get; set; }
        public string deleted { get; set; }
        public string _public { get; set; }
        public string mimetype { get; set; }
        public string pagecount { get; set; }
        public string rating_id { get; set; }
        public string rating_name { get; set; }
        public Rating[] ratings { get; set; }
        public string submission_type_id { get; set; }
        public string type_name { get; set; }
        public string guest_block { get; set; }
        public string friends_only { get; set; }
        public string comments_count { get; set; }
        public string views { get; set; }
        public object sales_description { get; set; }
        public string forsale { get; set; }
        public string digitalsales { get; set; }
        public string printsales { get; set; }
        public string digital_price { get; set; }
    }

    public class Keyword
    {
        public string keyword_id { get; set; }
        public string keyword_name { get; set; }
        public string contributed { get; set; }
        public string submissions_count { get; set; }
    }

    public class FilePID
    {
        public string file_id { get; set; }
        public string file_name { get; set; }
        public string file_url_full { get; set; }
        public string file_url_screen { get; set; }
        public string file_url_preview { get; set; }
        public string mimetype { get; set; }
        public string submission_id { get; set; }
        public string user_id { get; set; }
        public string submission_file_order { get; set; }
        public string full_size_x { get; set; }
        public string full_size_y { get; set; }
        public string screen_size_x { get; set; }
        public string screen_size_y { get; set; }
        public string preview_size_x { get; set; }
        public string preview_size_y { get; set; }
        public string initial_file_md5 { get; set; }
        public string full_file_md5 { get; set; }
        public string large_file_md5 { get; set; }
        public string small_file_md5 { get; set; }
        public string thumbnail_md5 { get; set; }
        public string deleted { get; set; }
        public string create_datetime { get; set; }
        public string create_datetime_usertime { get; set; }
        public string thumbnail_url_huge_noncustom { get; set; }
        public string thumbnail_url_large_noncustom { get; set; }
        public string thumbnail_url_medium_noncustom { get; set; }
        public string thumb_medium_noncustom_x { get; set; }
        public string thumb_medium_noncustom_y { get; set; }
        public string thumb_large_noncustom_x { get; set; }
        public string thumb_large_noncustom_y { get; set; }
        public string thumb_huge_noncustom_x { get; set; }
        public string thumb_huge_noncustom_y { get; set; }
    }

    public class Rating
    {
        public string content_tag_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string rating_id { get; set; }
    }


}
