const uri = 'api/flowers';
let flowers = [];

function getFlowers() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayFlowers(data))
        .catch(error => console.error('Unable to get flowers.', error));
}

function addFlower() {
    const addNameTextbox = document.getElementById('add-name');
    const addDescriptionTextbox = document.getElementById('add-description');

    const flower = {
        name: addNameTextbox.value.trim(),
        description: addDescriptionTextbox.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(flower)
    })
        .then(response => response.json())
        .then(() => {
            getFlowers();
            addNameTextbox.value = '';
            addDescriptionTextbox.value = '';
        })
        .catch(error => console.error('Unable to add flower.', error));
}


function deleteFlower(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getFlowers())
        .catch(error => console.error('Unable to delete flower.', error));
}

function displayEditForm(id) {
    const flower = flowers.find(flower => flower.FlowerId == id);
    document.getElementById('edit-id').value = flower.FlowerId;
    document.getElementById('edit-name').value = flower.FlowerName;
    document.getElementById('edit-description').value = flower.FlowerDescription;
    document.getElementById('editForm').style.display = 'block';
}

function updateFlower() {
    const flowerId = document.getElementById('edit-id').value;
    const flower = {
        id: parseInt(flowerId, 10),
        name: document.getElementById('edit-name').value.trim(),
        description: document.getElementById('edit-description').value.trim(),
    };

    fetch(`${uri}/${flowerId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(flower)
    })
        .then(() => getFlowers())
        .catch(error => console.error('Unable to update flower.', error));
    closeInput();
    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = `none`;
}

function _displayFlowers(data) {
    const tBody = document.getElementById('flowers');
    tBody.innerHTML = '';
    const button = document.createElement('button');
    data.forEach(flower => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${flower.FlowerId})`);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteFlower(${flower.FlowerId})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(flower.FlowerName);
        td1.appendChild(textNode);
        let td2 = tr.insertCell(1);
        let textNodeDescription = document.createTextNode(flower.FlowerDescription);
        td2.appendChild(textNodeDescription);
        let td4 = tr.insertCell(2);
        td4.appendChild(editButton);
        let td5 = tr.insertCell(3);
        td5.appendChild(deleteButton);
    });
    flowers = data;
}