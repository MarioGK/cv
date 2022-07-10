<script langt='ts'>
  export let visible = true
</script>

{#if visible}
  <i class="resume-award-icon fas fa-minus-square" data-fa-transform="shrink-2"></i>
{:else}
  <i class="resume-award-icon fas fa-plus-square" data-fa-transform="shrink-2"></i>
{/if}