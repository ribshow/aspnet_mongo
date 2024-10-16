var ButtonElement = document.querySelector('.button');
var ImageElement = document.querySelector('.expand-img');

ButtonElement.addEventListener('click', () => {

    const isExpanded = () => {
        ImageElement.style.width = '14rem';
        ImageElement.style.height = '14rem';
        ImageElement.style.transition = 'width 0.2s, height 0.2s';
        return true;
    }

    const isNotExpanded = () => {
        ImageElement.style.width = '10rem';
        ImageElement.style.height = '10rem';
        ImageElement.style.transition = 'width 0.2s, height 0.2s';
        return true;
    }

    if (isExpanded === true) {
        isNotExpanded();
        isExpanded = false;
    } else {
        isExpanded();
        isNotExpanded = false;
    }
});