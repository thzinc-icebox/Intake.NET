(function($) {
	$(function() {
		$("datalist.tags").each(function() {
			var tagList = $(this);
			var displayContainer = $("#" + tagList.data("displaycontainer"));
			var input = $("#" + tagList.data("input"));
			
			console.log(tagList, displayContainer, input);
			
			if (input.length) {
				input.on("keydown.intake", function(e) {
					var value = $(this).val().trim();
					console.log("keypress", value);
					if (e.keyCode == 13) {
						e.preventDefault();
						e.stopPropagation();
						
						var tagNameExists = tagList
							.find("option")
							.map(function() { return $(this).val(); })
							.toArray()
							.some(function(tagName) { return tagName == value; });

						if (!!value && !tagNameExists) {
							tagList.trigger("add", [ value ]);
							
							// Reset input
							$(this).val("");
						}
					}
				});
			}
			
			tagList
				.off(".intake")
				.on("display.intake", function() {
					displayContainer.empty();
					
					tagList.find("option").each(function() {
						var option = $(this);
						var tagName = option.val();
						
						var label = $('<label/>').text(tagName);
						var hiddenInput = $('<input type="hidden" name="tagName"/>').val(tagName);
						var removeButton = $('<a>Remove</a>').on("click", function() {
							tagList.trigger("remove", [ option ]);
						});
						var li = $('<li/>')
							.append(label)
							.append(hiddenInput)
							.append(removeButton);
						
						displayContainer.append(li);
					});
				})
				.on("add.intake", function(e, tagName) {
					console.log("add", tagName);
					var option = $("<option/>").val(tagName);
					tagList
						.append(option)
						.trigger("display");
				})
				.on("remove.intake", function(e, option) {
					tagList
						.remove(option)
						.trigger("display");
				})
				// Initial trigger
				.trigger("display");
		});
	});
}(jQuery))