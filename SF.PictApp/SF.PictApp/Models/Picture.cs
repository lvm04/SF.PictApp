using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SF.PictApp.Models
{
    public class Picture
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ImageSource Image { get; set; }
    }
}
