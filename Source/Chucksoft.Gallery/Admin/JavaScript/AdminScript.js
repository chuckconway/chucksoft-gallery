//Sets the current tab
window.onload = onLoad;

function confirm_delete(msg)
{
     return confirm(msg);
}


function getPageName(page)
{
	//Get the page url
    var href = page;
	
	//split the segments into an array from the forward slashes
	var slashsegments = href.split('/');
	
	//Get the last one.
	var pageNameAndQueryString =  slashsegments[slashsegments.length - 1];
	
	//assume there is a period seperating the page name and extension. This might not be true in the future. 
	var pageName = pageNameAndQueryString.split('.')[0];
	
	return pageName;
}

function onLoad()
{
    //set the tabs
    setSelectedTab();
    
    //Load LightBox
    initLightbox();

}

//
function setSelectedTab()
{
	//Get the primary navigation list and retrieve items into an array
	var primaryNavigation = document.getElementById('primarynavigation');
	var linklistElements = primaryNavigation.getElementsByTagName("li");
	
	//loop through the array looking for a match between the page name and the tab.
	for(var index = 0; linklistElements.length > index ; index++)
	{
		//get the first anchor in the list item. There will only be one.
		var anchor = linklistElements[index].getElementsByTagName("a")[0];
		
		//get the current and the current list item page name
		var currentPage = getPageName(document.URL);
		var selectedAnchor = getPageName(anchor.href);
		
		//compare the items, if they match operate on them.
		if(currentPage.toString().toLowerCase() == selectedAnchor.toString().toLowerCase())
		{
			//apply the active style to the current tab
			anchor.className = "active";
			
			//if it's the settings tab, hide the subNavigation. It does not apply to the Settings tab.
			if(currentPage.toString().toLowerCase() == "settings")
			{
				//hide the sub navigation
				var subNavigation = document.getElementById("subnavigation");
				subNavigation.style.visibility = "hidden";
			}
			
			//break out of the loop. No more work is needed.
			break;
		}
		
	}	
}
