
(function($) {
	$(function() {
		// Support for geolocation
		if (navigator && navigator.geolocation)
		{
			$(".location")
				.on("updatemap.intake", function(e) {
					var location = $(e.currentTarget);
					var latitude = location.data("latitude");
					var longitude = location.data("longitude");
					var accuracy = location.data("accuracy");
					
					if (!!latitude && !!longitude) {
						var mapOptions = {
							zoom: 15,
							mapTypeId: google.maps.MapTypeId.ROADMAP,
							center: new google.maps.LatLng(latitude, longitude)
						};
						var map = new google.maps.Map(location.find(".map").get(0), mapOptions);
					}
				})
				.trigger("updatemap")
				.on("click.intake", "a.get.location", function(e) {
					var button = $(e.currentTarget);
					var fieldset = $(e.delegateTarget);
					
					var latitude = fieldset.find(".latitude");
					var longitude = fieldset.find(".longitude");
					var accuracy = fieldset.find(".accuracy");
					
					var hasLocation = fieldset.hasClass("hasLocation");
				
					if (!hasLocation) {
						var success = function(position) {
							var latInput = $('<input type="hidden" name="latitude"/>').val(position.coords.latitude);
							var lonInput = $('<input type="hidden" name="longitude"/>').val(position.coords.longitude);
							var accInput = $('<input type="hidden" name="accuracy"/>').val(position.coords.accuracy);
							
							latitude
								.empty()
								.text(latInput.val())
								.append(latInput);
								
							longitude
								.empty()
								.text(lonInput.val())
								.append(lonInput);
								
							accuracy
								.empty()
								.text(accInput.val())
								.append(accInput);
								
							fieldset
								.addClass("hasLocation")
								.data("latitude", latInput.val())
								.data("longitude", lonInput.val())
								.data("accuracy", accInput.val())
								.trigger("updatemap");
						};
						
						var error = function() {
							alert("There was an error getting your current location.");
						};
						
						navigator.geolocation.getCurrentPosition(success, error, { enableHighAccuracy: true });
					}
					else {
						fieldset
							.removeClass("hasLocation")
							.removeData("latitude")
							.removeData("longitude")
							.removeData("accuracy");
						latitude.empty();
						longitude.empty();
						accuracy.empty();
					}
				});
		}
		else {
			// No support for in-browser geolocation, so disable the fieldset
			$("fieldset.location").addClass("disabled");
		}
		
		// Find any datalist with a class of tags and transform it into something pretty
		$("datalist.tags").each(function() {
			var tagList = $(this);
			var displayContainer = $("#" + tagList.data("displaycontainer"));
			var input = $("#" + tagList.data("input"));
			
			// If there's a tag input form, handle it
			if (input.length) {
				input.on("keydown.intake", function(e) {
					var value = $(this).val().trim();
					console.log("keypress", value);
					if (e.keyCode == 13) {
						e.preventDefault();
						e.stopPropagation();
						
						var values = value.split(/[ \+]+/gi);
						
						var i, il;
						for (i = 0, il = values.length; i < il; i++)
						{
							var splitValue = values[i];
						
							var tagNameExists = tagList
								.find("option")
								.map(function() { return $(this).val(); })
								.toArray()
								.some(function(tagName) { return tagName == splitValue; });

							if (!!splitValue && !tagNameExists) {
								tagList.trigger("add", [ splitValue ]);
								
								// Reset input
								$(this).val("");
							}
						}
					}
				});
				
				displayContainer.on("click", "a.remove", function() {
					console.log("clicked remove", $(this).data("tagName"));
					tagList.trigger("remove", [ $(this).data("tagName") ]);
				})
			}
			
			// Bind a few custom events on the tagList
			tagList
				// Unbind anything that might have been previously bound by this script
				.off(".intake")
				
				// Handle rebuilding the display of the tag list
				.on("display.intake", function() {
					console.log("display");
					displayContainer.empty();
					
					tagList.find("option").each(function() {
						var option = $(this);
						var tagName = option.val();
						
						var label = $('<a/>')
							.prop("href", "/data/tag/" + escape(tagName))
							.prop("target", "_blank")
							.text(tagName);
						var hiddenInput = $('<input type="hidden" name="tagName"/>').val(tagName);

						if (input.length) {
							var removeButton = $('<a class="remove">Remove</a>')
								.data("tagName", tagName);
						}
						
						var li = $('<li/>')
							.append(label)
							.append(hiddenInput)
							.append(removeButton);
						
						displayContainer.append(li);
					});
				})
				
				// Handle adding a tag to the tag list
				.on("add.intake", function(e, tagName) {
					console.log("add", tagName);
					var option = $("<option/>").val(tagName);
					tagList
						.append(option)
						.trigger("display");
				})
				
				// Handle removing a tag from the tag list
				.on("remove.intake", function(e, tagName) {
					tagList
						.find("option")
							.filter(function() {
								return $(this).val() == tagName;
							})
								.remove()
							.end()
						.end()
						.trigger("display");
				})
				// Initial trigger
				.trigger("display");
		});
	});
}(jQuery))