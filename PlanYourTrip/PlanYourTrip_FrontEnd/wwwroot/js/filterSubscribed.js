const allSubsContainer = document.getElementById("subcriptionsBox");
const filterInput = document.getElementById("filterInput");
const allSubs = allSubsContainer.getElementsByClassName("subscribedProfileBox")



filterInput.addEventListener("input", function (e) {
    FilterSubs();
});

function FilterSubs() {

    let regexValue = (filterInput.value.toLowerCase() == "") ? "*" : filterInput.value.toLowerCase();

    for (let i = 0; i < allSubs.length; i++) {
        let sub = allSubs[i].getElementsByClassName("userNick")[0].textContent.toLowerCase();

        if (filterInput.value.toLowerCase() == "") {
            allSubs[i].style.display = "block";
        }
        else {
            let re = new RegExp(filterInput.value.toLowerCase() + "+");

            if (sub.match(re)) {
                allSubs[i].style.display = "block";

            }
            else {
                allSubs[i].style.display = "none";
            }
        }
    }
}