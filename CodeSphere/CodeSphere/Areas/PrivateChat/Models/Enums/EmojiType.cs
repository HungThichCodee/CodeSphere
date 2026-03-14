using System.ComponentModel.DataAnnotations;

namespace CodeSphere.Areas.PrivateChat.Models.Enums
{
    public enum EmojiType
    {
        [Display(Name = "Smiles and People")]
        SmilesAndPeople = 1,
        [Display(Name = "Animals and Nature")]
        AnimalsAndNature = 2,
        [Display(Name = "Eat and Drink")]
        EatAndDrink = 3,
        [Display(Name = "Activities")]
        Activities = 4,
        [Display(Name = "Travel and Places")]
        TravelAndPlaces = 5,
        [Display(Name = "Objects")]
        Objects = 6,
        [Display(Name = "Symbols")]
        Symbols = 7,
        [Display(Name = "Flags")]
        Flags = 8,
    }
}
