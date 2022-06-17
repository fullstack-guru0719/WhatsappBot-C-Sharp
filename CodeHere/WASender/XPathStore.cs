using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASender
{
    public static class XPathStore
    {
        public static string GMap_Result = "//div[contains(@class,'Nv2PK')]";
        public static string GMap_Heading = "//h1[contains(@class,'DUwDvf')]";
        public static string GMap_MobileNumber = "//*[@data-tooltip='Copy phone number'] | //*[@data-tooltip='Copiar número de telefone'] | //button[starts-with(@data-item-id,'phone:tel:')]";
        //public static string GMap_MobileNumber = "//*[@data-tooltip='Copiar número de telefone'] ";
        public static string GMap_MobileNumberSecond="//*[@id=\"QA0Szd\"]/div/div/div[1]/div[3]/div/div[1]/div/div/div[2]/div[7]/div[4]/button/div[1]/div[2]/div[1]";
        public static string GMap_Address = "//*[@data-item-id='address' and (self::div or self::button)] | //span[@class='section-info-icon']/img[contains(@src,'/place_gm')]/ancestor::div[1] | //img[contains(@src,'/place_gm')]/ancestor::*[contains(@class,'button')][1]";
        public static string GMap_WebSite = "//*[@data-item-id='authority' and (self::div or self::button)] |  //span[@class='section-info-icon']/img[contains(@src,'/public_')]/ancestor::div[1] | //img[contains(@src,'/public_')]/ancestor::*[contains(@class,'button')][0]";
        public static string GMap_PlusCode = "//*[@data-item-id='oloc' and (self::div or self::button)] | //span[contains(@class,'plus-code')]/../.. | //img[contains(@src,'plus_code')]/ancestor::*[contains(@class,'button')][1]";
        public static string GMap_Rating = "//div[@jsaction='pane.rating.moreReviews']/span/span";
        public static string GMap_ReviewCount = "//button[@jsaction='pane.reviewChart.moreReviews']";
        public static string GMap_Catagory = "//button[@jsaction='pane.rating.category']";
        public static string GMap_NextButton = "//button[@jsaction='pane.paginationSection.nextPage']";



    }
}
