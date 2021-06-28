using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class EditStudentViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You have to enter a first name!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You have to enter a last name!")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You have to enter an e-mail!")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression(@"[0-9]{9,10}", ErrorMessage = "Only Digits. Min 9 - Max 10")]
        [Required(ErrorMessage = "You have to enter your phone number!")]
        public string Phone { get; set; }

        [Display(Name = "Street Adress")]
        [Required(ErrorMessage = "Please fill out your adress.")]
        public string Adress { get; set; }
    }
}