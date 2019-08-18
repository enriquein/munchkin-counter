export function selectAllText(element) {
    let el = element.target;
    el.setSelectionRange(0, el.value.length);
}
