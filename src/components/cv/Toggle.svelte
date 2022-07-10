<script>

    export let visible = true

</script>

{#if visible}
  <span>
    <i 
      class="toggle-on resume-award-icon fas fa-toggle-on" 
      data-fa-transform="shrink-2"></i>
    </span>
{:else}
  <span>
    <i 
      class="resume-award-icon fas fa-toggle-off" 
      data-fa-transform="shrink-2"></i>
    </span>
{/if}

<style>
  span {
    cursor: pointer;
  }
</style>