<script type='ts'>
  import type { CurriculumCareerType } from "./Curriculum.type";

  export let career: CurriculumCareerType
</script>

<section class="resume-section summary-section mb-5">
  <h2 class="resume-section-title text-uppercase font-weight-bold pb-3 mb-3">{ career.title || 'Career Summary' } </h2>
  <div class="resume-section-content">
    <p class="mb-0">{ career.summary }</p>
  </div>
</section><!--//summary-section-->